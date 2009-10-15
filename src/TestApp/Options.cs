using System;
using Optional.Attributes;

namespace TestApp
{
	public class Options
	{
		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		public bool Flag { get; set; }

		// ShortName has to be set for this property, because it starts with 'n', just like Name
		[ShortName("i"), LongName("non-interactive")]
		public string NonInteractive { get; set; }

		[Required] // This property is ignored, because it is internal
			internal string Hidden { get; set; }

		public override string ToString()
		{
			return String.Format("Name: {1}{0}Description: {2}{0}Flag: {3}{0}Non-interactive: {4}", Environment.NewLine, Name, Description, Flag, NonInteractive);
		}
	}
}