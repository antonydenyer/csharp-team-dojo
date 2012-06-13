using System.Xml.Serialization;

namespace TeamDojo.Web.Models
{
	public class Recommendation
	{
		[XmlText]
		public string Name { get; set; }
	}
}