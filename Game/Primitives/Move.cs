namespace Game.Primitives
{
    public readonly struct Move
    {
        public Cell From { get; }

        public Cell To { get; }

        public Move(byte x0, byte y0, byte x1, byte y1)
        {
            From = new Cell(x0, y0);
            To = new Cell(x1, y1);
        }

        public Move(Cell from, Cell to)
        {
            From = from;
            To = to;
        }

        public override string ToString()
        {
            return $"{From.X}, {From.Y} -> {To.X}, {To.Y}";
        }
    }
}
