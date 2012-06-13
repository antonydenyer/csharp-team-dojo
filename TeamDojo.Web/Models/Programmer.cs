using System.Collections.Generic;
using System.Xml.Serialization;

namespace TeamDojo.Web.Models
{
	public class Programmer
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlArray("Recommendations")]
		public List<Recommendation> Recommendations { get; set; }

		[XmlArray("Skills")]
		public List<Skill> Skills { get; set; }

		public decimal Kudos { get; set; }
	}
}