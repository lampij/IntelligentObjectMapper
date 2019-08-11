using System;
using NUnit.Framework;
using IOM.Internals.Readers;
using Newtonsoft.Json;

namespace Tests.MetadataReaderTests
{
    [TestFixture]
    public class MetadataReaderTests{
        
        //Sanity check to ensure tests are building properly.
        [TestCase]
        public void UnfailableTests(){
            Console.WriteLine($"Running UnfailableTests");
            Assert.IsTrue(true);
        }

        /*
        So let's describe what this reader should do.
        It should
        [ ] Take an object, or already serialized version of an object
             [ ] Understand how to read those specific formats 
        */

        [TestCase]
        public void ObjectConstructor(){
            //We should be able to generate a reader from a simple object.
            //The object here should be simple enough; make sure what we serialize, we can deserialize.
            MetadataReader<DateTime> reader = new MetadataReader<DateTime>(DateTime.Now);
            DateTime d = JsonConvert.DeserializeObject<DateTime>(reader._InternalSerializedObject);
            
            Assert.IsNotNull(d);
        }
        
        [TestCase]
        public void StaticObjectConstructor(){
            //We should also be able to generate a reader from a previously serialized object
            MetadataReader<DateTime> readerFromSerializedObject = MetadataReader<DateTime>.From(JsonConvert.SerializeObject(DateTime.Now));

            //Honestly, I think it's good enough to simply check that these three things are the same. 
            //This would be after round trip serialization.
            Assert.IsTrue(readerFromSerializedObject._InternalDeserializedObject.Day == DateTime.Now.Day);
            Assert.IsTrue(readerFromSerializedObject._InternalDeserializedObject.Month == DateTime.Now.Month);
            Assert.IsTrue(readerFromSerializedObject._InternalDeserializedObject.Year == DateTime.Now.Year);
        }
    }
}