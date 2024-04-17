using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace LSP.Core.Utilities.CustomAttributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NotEmptyList : RequiredAttribute
	{
		public override bool IsValid(object value) => (value as IEnumerable)?.GetEnumerator().MoveNext() ?? false;
	}
}
