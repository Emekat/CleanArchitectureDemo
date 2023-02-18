using ShoppingCart.API.EventFeed;

namespace ShoppingCart.API;
public interface IEventStore
{
    IEnumerable<Event> GetEvents(
      long firstEventSequenceNumber, long lastEventSequenceNumber);
    void Raise(string eventName, object content);
  
}

 public class EventStore : IEventStore
    {
        // omitted in this chapter
        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber,long lastEventSequenceNumber)
        {
        //    return database.Where(e =>
        //     e.SequenceNumber >= firstEventSequenceNumber &&
        //     e.SequenceNumber <= lastEventSequenceNumber)
        //   .OrderBy(e => e.SequenceNumber);
        return new List<Event>
        {
           new Event(1, DateTimeOffset.UtcNow,"Event was raised", new {b = "a"})
        };
        
        }


        public void Raise(string eventName, object content)
        {
            // var seqNumber = database.NextSequenceNumber();
            // database.Add(new Event(seqNumber,DateTimeOffset.UtcNow,eventName,content));
            Console.WriteLine("Event was raised");
        }
    }
