using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IObservableExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define a provider and two observers.
            LocationTracker provider = new LocationTracker();
            LocationReporter reporter1 = new LocationReporter("FixedGPS");
            reporter1.Subscribe(provider);
            LocationReporter reporter2 = new LocationReporter("MobileGPS");
            reporter2.Subscribe(provider);

            provider.TrackLocation(new Location(47.6456, -122.1312));
            reporter1.Unsubscribe();
            provider.TrackLocation(new Location(47.6677, -122.1199));
            provider.TrackLocation(null);
            provider.EndTransmission();

        }
    }

    public class LocationReporter : IObserver<Location>
    {
        private IDisposable unsubscriber;
        private string instName;

        public LocationReporter(string name)
        {
            this.instName = name;
        }

        public string Name
        { get { return this.instName; } }

        public virtual void Subscribe(IObservable<Location> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void OnCompleted()
        {
            Console.WriteLine("The Location Tracker has completed transmitting data to {0}.", this.Name);
            this.Unsubscribe();
        }

        public virtual void OnError(Exception e)
        {
            Console.WriteLine("{0}: The location cannot be determined.", this.Name);
        }

        public virtual void OnNext(Location value)
        {
            Console.WriteLine("{2}: The current location is {0}, {1}", value.Latitude, value.Longitude, this.Name);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }
    }


    public class LocationTracker : IObservable<Location>
    {
        private List<IObserver<Location>> observers;

        public LocationTracker()
        {
            observers = new List<IObserver<Location>>();
        }

        public IDisposable Subscribe(IObserver<Location> observer)
        {
            if (! observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Location>> _observers;
            private IObserver<Location> _observer;

            public Unsubscriber(List<IObserver<Location>> obervers, IObserver<Location> observer)
            {
                this._observers = obervers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void TrackLocation(Nullable<Location> loc)
        {
            foreach (var observer in observers)
                if (!loc.HasValue)
                    observer.OnError(new LocationUnknownException());
                else
                    observer.OnNext(loc.Value);
        }

        public void EndTransmission()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();
            observers.Clear();
        }
    }

    public class LocationUnknownException  : Exception
    {
        internal LocationUnknownException() { }
    }

    public struct Location
    {
        double lat, lon;

        public Location(double latitude,double longitude)
        {
            this.lat = latitude;
            this.lon = longitude;
        }

        public double Latitude
        {
            get
            {
                return this.lat;
            }
        }

        public double Longitude
        {
            get
            {
                return this.lon;
            }
        }
    }
}
