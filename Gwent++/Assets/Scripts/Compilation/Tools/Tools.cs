using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Compiler
{
    public static class Tools
    {
        /// <summary>
        /// Provides a dictionary that defines the precedence of different token types.
        /// This is useful for determining the order of operations in expressions for the AST.
        /// </summary>
        public static Dictionary <TokenType, int> GetPrecedence = new()
        {
            { TokenType.And, 1 },
            { TokenType.Or, 1 },
            { TokenType.Equal, 2 },
            { TokenType.LessEq, 2 },
            { TokenType.MoreEq, 2 },
            { TokenType.More, 2 },
            { TokenType.Less, 2 },
            { TokenType.Plus, 3 },
            { TokenType.Minus, 3 },
            { TokenType.Concatenation, 3 },
            { TokenType.SpaceConcatenation, 3 },
            { TokenType.Multiply, 4 },
            { TokenType.Divide, 4 },
            { TokenType.Not, 4 },
            { TokenType.Pow, 5 },
            { TokenType.Point, 6 }
        };

        /// <summary>
        /// Determines the value type associated with a given token type.
        /// This method is useful for identifying the expected type of a token in expressions.
        /// </summary>
        /// <returns>The corresponding value type if recognized; otherwise, null.</returns>
        public static ValueType? GetKeywordType(TokenType token)
        {
            return token switch
            {
                // Strings
                TokenType.Name => ValueType.String,
                TokenType.Faction => ValueType.String,
                TokenType.Type => ValueType.String,
                TokenType.StringType => ValueType.String,
                TokenType.Source => ValueType.String,
                TokenType.EffectParam => ValueType.String,

                // Players
                TokenType.Owner => ValueType.Player,
                TokenType.TriggerPlayer => ValueType.Player,

                // Numbers
                TokenType.Power => ValueType.Int,
                TokenType.Plus => ValueType.Int,
                TokenType.Minus => ValueType.Int,
                TokenType.RIncrement => ValueType.Int,
                TokenType.LIncrement => ValueType.Int,
                TokenType.RDecrement => ValueType.Int,
                TokenType.LDecrement => ValueType.Int,
                TokenType.NumberType => ValueType.Int,

                // Predicates
                TokenType.Predicate => ValueType.Predicate,

                // Booleans
                TokenType.Not => ValueType.Bool,
                TokenType.Bool => ValueType.Bool, 
                TokenType.Single => ValueType.Bool,

                // List Cards
                TokenType.Deck => ValueType.CardCollection,
                TokenType.DeckOfPlayer => ValueType.CardCollection,
                TokenType.GraveYard => ValueType.CardCollection,
                TokenType.GraveYardOfPlayer => ValueType.CardCollection,
                TokenType.Field => ValueType.CardCollection,
                TokenType.FieldOfPlayer => ValueType.CardCollection,
                TokenType.Hand => ValueType.CardCollection,
                TokenType.HandOfPlayer => ValueType.CardCollection,
                TokenType.Board => ValueType.CardCollection,
                TokenType.Find => ValueType.CardCollection,

                // Cards
                TokenType.Pop => ValueType.Card,

                // Voids
                TokenType.SendBottom => ValueType.Void,
                TokenType.Push => ValueType.Void,
                TokenType.Shuffle => ValueType.Void,
                TokenType.Add => ValueType.Void,
                TokenType.Remove => ValueType.Void,

                _ => null,
            };
        }

        /// <summary>
        /// Determines the value type associated with a given operator token.
        /// This method is useful for identifying the return type of the left and right of an operation based on its token.
        /// </summary>
        /// <returns>The corresponding value type if recognized; otherwise, null.</returns>
        public static ValueType? GetOperatorType(TokenType token)
        {
            return token switch
            {
                TokenType.PlusEqual => ValueType.Int,
                TokenType.MinusEqual => ValueType.Int,
                TokenType.Plus => ValueType.Int,
                TokenType.Minus => ValueType.Int,
                TokenType.Multiply => ValueType.Int,
                TokenType.Divide => ValueType.Int,
                TokenType.Pow => ValueType.Int,
                TokenType.LessEq => ValueType.Int,
                TokenType.MoreEq => ValueType.Int,
                TokenType.Less => ValueType.Int,
                TokenType.More => ValueType.Int,
                TokenType.And => ValueType.Bool,
                TokenType.Or => ValueType.Bool,
                TokenType.Concatenation => ValueType.String,
                TokenType.SpaceConcatenation => ValueType.String,
                _ => null,
            };
        }


        /// <summary>
        /// Returns a list of token types that represent possible methods or properties
        /// available for a given value type. This is useful for method resolution in expressions.
        /// </summary>
        /// <returns>A list of token types representing methods or properties for the given value type.</returns>
        public static List<TokenType> GetPossibleMethods(ValueType? value)
        {
            return value switch
            {
                ValueType.Card => new[] { TokenType.Name, TokenType.Owner, TokenType.Power, TokenType.Faction, TokenType.Range, TokenType.Type }.ToList(),
                ValueType.Context => new[] { TokenType.Deck, TokenType.DeckOfPlayer, TokenType.GraveYard, TokenType.GraveYardOfPlayer, TokenType.Field, TokenType.FieldOfPlayer, TokenType.Hand, TokenType.HandOfPlayer, TokenType.Board, TokenType.TriggerPlayer }.ToList(),
                ValueType.CardCollection => new[] { TokenType.Find, TokenType.Push, TokenType.SendBottom, TokenType.Pop, TokenType.Shuffle, TokenType.Add, TokenType.Remove }.ToList(),
                _ => new(),
            };
        }

        /// <summary>
        /// Provides a list of variable types that are recognized in the current context.
        /// This list is used to validate and enforce type consistency during code analysis.
        /// </summary>
        public static List<ValueType?> VariableTypes = new List<ValueType?>
        {
            ValueType.Int,
            ValueType.Bool,
            ValueType.Unassigned,
            ValueType.String
        };
    }
}