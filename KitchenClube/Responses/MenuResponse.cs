namespace KitchenClube.Responses;
public record MenuResponse(Guid Id, DateTime StartDate, DateTime EndDate, MenuStatus Status);