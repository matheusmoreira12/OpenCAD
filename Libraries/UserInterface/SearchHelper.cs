using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UserInterface
{
    static class Scores
    {
        public const int OFFSET_CHARACTER = -100;
        public const int CHARACTER_TYPE_MATCH = 10;
        public const int CHARACTER_CASE_MATCH = 5;
    }

    public enum CharMatchMode { NoMatch = 0, FullMatch = 3, MatchesCase = 1, MatchesType = 2 }

    public static class CharMatchModeExtension
    {
        public static int GetScore(this CharMatchMode mode)
        {
            int score = 0;

            if (mode.HasFlag(CharMatchMode.MatchesCase)) score += Scores.CHARACTER_CASE_MATCH;

            if (mode.HasFlag(CharMatchMode.MatchesType)) score += Scores.CHARACTER_TYPE_MATCH;

            return score;
        }
    }

    public static class CharExtension
    {
        public static bool TypesMatch(this char a, char b) => char.IsLower(a) == char.IsLower(b);

        public static bool CasesMatch(this char a, char b) => char.ToLowerInvariant(a) == char.ToLowerInvariant(b);

        public static CharMatchMode MatchTo(this char a, char b)
        {
            if (a.TypesMatch(a))
            {
                if (a.CasesMatch(b)) return CharMatchMode.FullMatch;

                return CharMatchMode.MatchesType;
            }

            return CharMatchMode.NoMatch;
        }
    }

    public struct WordMatch
    {
        private IEnumerable<CharMatchMode>matchChars(string content, string term, int offset)
        {
            if (string.IsNullOrEmpty(content)) yield break;

            if (string.IsNullOrEmpty(term)) yield break;

            for (int i = 0, j = i + offset; i < term.Length && j < content.Length; i++)
                yield return term[i].MatchTo(content[j]);
        }

        private void initialize(string content, string term, out IEnumerable<CharMatchMode> matches)
        {
            char firstChar = term[0];

            matches = matchChars(content, term, Offset).ToArray();
        }

        public WordMatch(string content, string term, int offset) : this()
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
            SearchString = term ?? throw new ArgumentNullException(nameof(term));

            IEnumerable<CharMatchMode> matches;

            initialize(Content, SearchString, out matches);

            Offset = offset;
            Matches = matches.ToArray();
        }

        public int GetRelevanceScore()
        {
            int score = 0;

            score += Offset * Scores.OFFSET_CHARACTER;

            foreach (var match in Matches) score += match.GetScore();

            return score;
        }

        public string Content { get; }
        public string SearchString { get; }
        public int Offset { get; }
        public CharMatchMode[] Matches { get; private set; }
    }

    public static class SearchHelper
    {
        public static int GetRelevanceScore<T>(string term, T item, Func<T, string> fieldSelector)
            => new WordMatch(fieldSelector(item), term, 0).GetRelevanceScore();

        public static IEnumerable<T> SortByRelevance<T>(IEnumerable<T> items, string term,
            Func<T, string> fieldSelector)
        {
            if (items == null) return null;

            var ordered = items.OrderByDescending(item => GetRelevanceScore(term, item, fieldSelector));

            return ordered;
        }

        public static IEnumerable<T> FilterByRelevance<T>(IEnumerable<T> items, string term,
            Func<T, string> fieldSelector)
        {
            var scores = items.Select(item => GetRelevanceScore(term, item, fieldSelector));

            int minScore = scores.Min();

            var filtered = items.TakeWhile(item => GetRelevanceScore(term, item, fieldSelector) > minScore);

            return filtered;
        }

        public static IEnumerable<T> FilterAndSortByRelevance<T>(IEnumerable<T> items, string term,
            Func<T, string> fieldSelector)
        {
            var filtered = FilterByRelevance(items, term, fieldSelector);

            var sorted = SortByRelevance(filtered, term, fieldSelector);

            return sorted;
        }
    }
}