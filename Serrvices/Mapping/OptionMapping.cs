using MongoDB.Bson.Serialization;

namespace MongoDB_Demo.Serrvices.Mapping
{
    public class OptionMapping
    {
        public static void Register()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Models.Option)))
            {
                BsonClassMap.RegisterClassMap<Models.Option>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                    cm.MapMember(c => c.Id).SetElementName("_id");
                    cm.MapMember(c => c.PollId).SetElementName("pollId");
                    cm.MapMember(c => c.Text).SetElementName("text");
                    cm.MapMember(c => c.Votes).SetElementName("votes");
                });
            }
        }
    }
}
