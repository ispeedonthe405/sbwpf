namespace sbwpf.Controls
{
    public interface IControlSerializer
    {
        public void Serialize(string controlId, string parameter, string value);
        public string Deserialize(string controlId, string parameter);
    }
}
