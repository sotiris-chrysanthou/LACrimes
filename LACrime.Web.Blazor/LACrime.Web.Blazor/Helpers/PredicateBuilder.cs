using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace LACrimes.Web.Blazor.Server.Helpers {
    internal class PredicateBuilder {
        internal static Expression<Func<T, bool>>? BuildPredicate<T>(string predicateStr) {
            // Using System.Linq.Dynamic.Core to parse complex expressions
            if(predicateStr == "null") {
                return null;
            }
            return DynamicExpressionParser.ParseLambda<T, bool>(new ParsingConfig(), false, predicateStr);
        }
    }
}
