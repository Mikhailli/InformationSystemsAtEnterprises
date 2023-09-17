namespace lab1;

public class EventTable
{
    public List<EventTableLine> EventTableLines { get; set; }

    public EventTable(WorkTable workTable, List<int> to, List<int> inElements, List<int> outElements)
    {
        var lines = new List<EventTableLine>();
        
        var improveTo = to.Select((t, i) => (i + 1, t)).ToList();

        var events = improveTo.OrderBy(_ => _.Item2).ThenBy(_ => _.Item1).Select(_ => _.Item1);

        foreach (var _ in events)
        {
            var eventTableLine = new EventTableLine();
            eventTableLine.Event = _;
            eventTableLine.EarlyDeadline = inElements.Contains(_) 
                ? 0 
                : workTable.Lines.Where(__ => __.WorkTime.EndPoint == _).Select(__ => __.PCOP).Max();

            if (outElements.Contains(_))
            {
                eventTableLine.LateDeadline = workTable.Lines.MaxBy(__ => __.ПCOP)!.ПCOP;
            }
            else
            {
                eventTableLine.LateDeadline = workTable.Lines.Where(__ => __.WorkTime.StartPoint == _)
                    .Select(__ => __.ПCHP).Min();
            }

            eventTableLine.Reserve = eventTableLine.LateDeadline - eventTableLine.EarlyDeadline;
            
            lines.Add(eventTableLine);
        }

        EventTableLines = lines;
    }
}