namespace WebApplicationBackend.Entities
{
    //STEP 1-> Create console app / asp.net core api app
    //STEP 2-> install these packages:
    /*
     Microsoft.EntityFrameworkCore.SqlServer  //use connect to database EF core
     Microsoft.EntityFrameworkCore.Tools // use to run migration commands and create/update database structure
     */

    //STEP 3-> to create entity class
    public class Employee
    {
        public int Id { get; set; } //EEF Core convention se isko Primary Key maanta hai. default
        public string? Name { get; set; }
        public decimal Salary { get; set; }
    }
}
























/*
 | Table Name       | Purpose                 |
| ---------------- | ----------------------- |
| `Members`        | Party members           |
| `Leaders`        | Senior leaders          |
| `Departments`    | Party departments       |
| `States`         | Indian states           |
| `Districts`      | District data           |
| `Constituencies` | Election constituencies |
| `Elections`      | Election details        |
| `Candidates`     | Election candidates     |
| `Voters`         | Voter information       |
| `Booths`         | Polling booths          |
| `Volunteers`     | Campaign volunteers     |
| `Donations`      | Funding records         |
| `Meetings`       | Party meetings          |
| `Events`         | Political events        |
| `Campaigns`      | Election campaigns      |
| `Complaints`     | Public complaints       |
| `Manifestos`     | Party manifestos        |
| `MediaPosts`     | Social media/news posts |
| `Users`          | Login users             |
| `Roles`          | Admin/User roles        |

1. Members Table
CREATE TABLE Members
(
    MemberId INT PRIMARY KEY IDENTITY,

    FullName VARCHAR(100),

    Age INT,

    Gender VARCHAR(20),

    Phone VARCHAR(15),

    Email VARCHAR(100),

    StateId INT,

    JoinDate DATE
)
2. States Table
CREATE TABLE States
(
    StateId INT PRIMARY KEY IDENTITY,

    StateName VARCHAR(100)
)
3. Districts Table
CREATE TABLE Districts
(
    DistrictId INT PRIMARY KEY IDENTITY,

    DistrictName VARCHAR(100),

    StateId INT
)
4. Elections Table
CREATE TABLE Elections
(
    ElectionId INT PRIMARY KEY IDENTITY,

    ElectionName VARCHAR(100),

    ElectionYear INT,

    ElectionType VARCHAR(50)
)
5. Candidates Table
CREATE TABLE Candidates
(
    CandidateId INT PRIMARY KEY IDENTITY,

    CandidateName VARCHAR(100),

    Age INT,

    PartyName VARCHAR(100),

    ConstituencyId INT,

    ElectionId INT
)
6. Constituencies Table
CREATE TABLE Constituencies
(
    ConstituencyId INT PRIMARY KEY IDENTITY,

    ConstituencyName VARCHAR(100),

    StateId INT
)
7. Donations Table
CREATE TABLE Donations
(
    DonationId INT PRIMARY KEY IDENTITY,

    DonorName VARCHAR(100),

    Amount DECIMAL(12,2),

    DonationDate DATE
)
8. Meetings Table
CREATE TABLE Meetings
(
    MeetingId INT PRIMARY KEY IDENTITY,

    MeetingTitle VARCHAR(200),

    MeetingDate DATETIME,

    Location VARCHAR(200)
)
9. Campaigns Table
CREATE TABLE Campaigns
(
    CampaignId INT PRIMARY KEY IDENTITY,

    CampaignName VARCHAR(100),

    StartDate DATE,

    EndDate DATE,

    Budget DECIMAL(12,2)
)
10. Volunteers Table
CREATE TABLE Volunteers
(
    VolunteerId INT PRIMARY KEY IDENTITY,

    FullName VARCHAR(100),

    Phone VARCHAR(15),

    City VARCHAR(100)
)
 */