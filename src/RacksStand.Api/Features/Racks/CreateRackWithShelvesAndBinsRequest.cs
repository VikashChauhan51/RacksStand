namespace RacksStand.Api.Features.Racks;

public record CreateRackWithShelvesAndBinsRequest
(
    RackRequest Rack
);

public record RackRequest
(
   string Name,
   string Code,
   string? Label,
   string? Description,
   float XAxis,
   float YAxis,
   decimal Height,
   decimal Width,
   decimal Length,
   string DiminsionUnit,
   IEnumerable<ShelfRequest> Shelves
);

public record ShelfRequest
(
     string Name,
     string Code,
     string? Label,
     string? Level,
     string? Description,
     float XAxis,
     float YAxis,
     decimal Height,
     decimal Width,
     decimal Length,
     string DiminsionUnit,
     IEnumerable<BinRequest> Bins
);

public record BinRequest
(
    string Name,
    string Code,
    string? Label,
    string? Level,
    string? Description,
    string? Barcode,
    float XAxis,
    float YAxis,
    decimal Height,
    decimal Width,
    decimal Length,
    string DiminsionUnit,
    string? BarcodeFormat
);
