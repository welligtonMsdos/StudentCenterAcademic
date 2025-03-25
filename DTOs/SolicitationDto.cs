namespace StudentCenterAcademic.DTOs;

public class SolicitationDto
{
    public int Id { get; init; }
    public int StudentId { get; init; }
    public string Description { get; init; }
    public string DescriptionStatus { get; init; }
    public string DescriptionRequestType { get; init; }

    public SolicitationDto()
        : this(0, 0, string.Empty, string.Empty, string.Empty) { }

    public SolicitationDto(int id,
                           int studentId,
                           string description,
                           string descriptionStatus,
                           string descriptionRequestType)
    {
        Id = id;
        StudentId = studentId;
        Description = description;
        DescriptionStatus = descriptionStatus;
        DescriptionRequestType = descriptionRequestType;
    }
}
