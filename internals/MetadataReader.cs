/* The metadata reader is responsible for doing recon within the model we're working with. 
It should do some basic things like serialize to a known, workable format (probably json), 
and then from that point derive the information we're going to need to complete the looking between 
two given models.

TODO:
    [X] Serialize a given object
    [ ] Understand the relationships between nested objects and their parents
        [ ] i.e does a 'kitchen' object always have a 'sink' object
    [ ] Do natural keys exist in the given data?
    [ ] Output to some kind of data readable by the rest of the system (probably xml, now that I'm thinking about what we'll need)
 */

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IOM.Internals.Readers
{
    public class MetadataReader<T>
    {

        /*We will have to decide at some point if is worth keeping state in this class. */
        public string _InternalSerializedObject { get; set; }
        public T _InternalDeserializedObject { get; set; }

        #region Constructors & Init
        public MetadataReader(T obj)
        {
            _InternalDeserializedObject = obj;
            _InternalSerializedObject = JsonConvert.SerializeObject(obj);
        }

        public static MetadataReader<T> From(string serializedData)
        {
            //I guess for now we'll just try to deserialize JSON? That's a pretty achievable format for most back ends nowadays.
            try
            {
                return new MetadataReader<T>((T)JsonConvert.DeserializeObject(serializedData));
            }
            catch (System.Exception e)
            {
                //TODO: Implement some kind of error handling
                throw e;
            }
            //TODO: 
            // Somehow, we need to handle situations where we don't know the model we're working with.
            // Maybe a class generator? 
            throw new NotImplementedException();
        }
        #endregion


        /*Okay so we need to to do some object relationship mapping here */
        /*Ideally, we want to split this up into the smallest possible problems. */
        /*Also, we're going to need to build some kind of 'relationship' model...but we'll get to that later, for now, maybe just some string data will be fine.*/

        public static void GetRelationships(string jsonObject){
            JObject strippedObject = (JObject)JsonConvert.DeserializeObject(jsonObject);
            foreach (var item in strippedObject)
            {
                Console.Write(item.Value);
            }
        }
        
    }
}