namespace Fanex.Result.ThirdParty.Models
{
    public class Call
    {
        public Call(
            string seq,
            int vendorId)
        {
            Seq = seq;
            VendorId = vendorId;
        }

        public string Seq { get; }

        public int VendorId { get; }
    }
}
