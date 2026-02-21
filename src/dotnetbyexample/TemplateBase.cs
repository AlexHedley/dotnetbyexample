// A class to be used as the base class for the generated template.
using System.Text;

namespace Nocco {
	public abstract class TemplateBase {

		// Properties available from within the template
		public string Title { get; set; } = String.Empty;
        public string PathToCss { get; set; } = String.Empty;
        public string PathToJs { get; set; } = String.Empty;
        public Func<string, string> GetSourcePath { get; set; }
		public List<Section> Sections { get; set; } = new();
		public List<string> Sources { get; set; } = new();

        public StringBuilder Buffer { get; set; }

		protected TemplateBase() {
			Buffer = new StringBuilder();
		}

		// This `Execute` function will be defined in the inheriting template
		// class. It generates the HTML by calling `Write` and `WriteLiteral`.
		public abstract void Execute();

		public virtual void Write(object value) {
			WriteLiteral(value);
		}

		public virtual void WriteLiteral(object value) {
			Buffer.Append(value);
		}
	}
}
