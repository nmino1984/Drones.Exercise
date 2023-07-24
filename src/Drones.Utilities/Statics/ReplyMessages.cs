namespace Drones.Utilities.Statics
{
    public class ReplyMessages
    {
        public const string MESSAGE_QUERY = "Succeed Query!";
        public const string MESSAGE_QUERY_EMPTY = "No Items found!";
        public const string MESSAGE_SAVE = "Item Saved Successfully";
        public const string MESSAGE_UPDATE = "Item Updated Successfully";
        public const string MESSAGE_DELETE = "Item Deleted Successfully";
        public const string MESSAGE_EXIST = "Item Exists already.";
        public const string MESSAGE_ACTIVATE = "Item has been Activated";
        public const string MESSAGE_TOKEN = "Token generated Successfully";
        public const string MESSAGE_VALIDATE = "Errors on the Validation";
        public const string MESSAGE_FAILED = "Operation Failed";

        public const string MESSAGE_WRONG_DRONE_ID = "The Drone Id given was not found!";
        public const string MESSAGE_EMPTY_MEDICATIONS = "There're no Medications to Load the Drone!";
        public const string MESSAGE_DRONE_NOT_LOADED = "The Drone is Not Loaded!";
        public const string MESSAGE_NOT_DRONE_AVAILABLE = "Not Drone Available at this moment!";
    }

    public static class SerialGenerate
    {
        public static string Generate() 
        {
            Guid guid = Guid.NewGuid();

            return guid.ToString();
        }
    }
}
