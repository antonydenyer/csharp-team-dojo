using System.Xml.Serialization;

namespace TeamDojo.Web.Models
{
	public class Skill
	{
		[XmlText]
		public string Value { get; set; }
	}
}