﻿using System;
using System.IO;

namespace Garbage
{
    public struct State
    {
        public readonly int Groups;
        public readonly int NestingLevel;
        public readonly int Score;
        public readonly int GarbageCount;

        public State(int groups, int nestingLevel, int score, int garbageCount)
        {
            Groups = groups;
            NestingLevel = nestingLevel;
            Score = score;
            GarbageCount = garbageCount;
        }

        public State StartGroup() => new State(Groups + 1, NestingLevel + 1, Score, GarbageCount);
        public State EndGroup() => new State(Groups, NestingLevel - 1, Score + NestingLevel, GarbageCount);
        public State Garbage() => new State(Groups, NestingLevel, Score, GarbageCount + 1);
        public override string ToString() => $"Groups = {Groups}, Score = {Score}, Garbage = {GarbageCount}";

    }

    public class GarbageProcessor
    {
        private Func<char, State, State> _process;

        State Counting(char c, State state)
        {
            switch (c)
            {
                case '{':
                    return state.StartGroup();
                case '}':
                    return state.EndGroup();
                case '<':
                    _process = ProcessingGarbage;
                    break;
            }
            return state;
        }

        private State EscapingOneCharacter(char c, State state)
        {
            _process = ProcessingGarbage;
            return state;
        }

        private State ProcessingGarbage(char c, State state)
        {
            switch (c)
            {
                case '!':
                    _process = EscapingOneCharacter;
                    return state;
                case '>':
                    _process = Counting;
                    return state;
            }
            return state.Garbage();
        }

        public State Process(string input)
        {
            var reader = new StringReader(input);
            return Process(reader);
        }

        public State Process(TextReader reader)
        {
            _process = Counting;
            var state = new State();
            while (reader.Peek() >= 0)
            {
                var c = (char)reader.Read();
                state = _process(c, state);
            }
            return state;
        }
    }
}