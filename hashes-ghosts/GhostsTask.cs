using System;
using System.Text;

namespace hashes
{

    public class GhostsTask :
        IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
        IMagic
    {
        private byte[] bytes;
        private Vector vector;
        private Segment segment;
        private Cat cat;

        public void DoMagic()
        {
            vector?.Add(new Vector(1, 1));
            segment?.Start.Add(new Vector(1, 1));
            Robot.BatteryCapacity++;
            cat?.Rename("AnotherName");
            if (bytes != null) bytes[0]++;
        }

        Document IFactory<Document>.Create()
        {
            if(bytes == null) bytes = new byte[] {1,1,1,1,1,1};
            return new Document("Title", Encoding.UTF8, bytes);
        }

        Vector IFactory<Vector>.Create()
        {
            return vector ?? (vector = new Vector(1, 1));
        }

        Segment IFactory<Segment>.Create()
        {
            return segment ?? (segment = new Segment(new Vector(0, 0), new Vector(-1, -1)));
        }

        Cat IFactory<Cat>.Create()
        {
            return cat ?? (cat = new Cat("Name", "Breed", DateTime.Today));
        }

        Robot IFactory<Robot>.Create()
        {
            return new Robot("Test", 1);
        }
    }

}