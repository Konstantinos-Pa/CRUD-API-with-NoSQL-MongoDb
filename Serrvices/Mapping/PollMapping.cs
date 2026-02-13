namespace MongoDB_Demo.Serrvices.Mapping
{
    public class PollMapping
    {
        public static void Register()
        {
            if (!MongoDB.Bson.Serialization.BsonClassMap.IsClassMapRegistered(typeof(Models.Poll)))
            {
                MongoDB.Bson.Serialization.BsonClassMap.RegisterClassMap<Models.Poll>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                    cm.MapMember(c => c.id).SetElementName("_id");
                    cm.MapMember(c => c.Question).SetElementName("question");
                });
            }
        }
    }
}
