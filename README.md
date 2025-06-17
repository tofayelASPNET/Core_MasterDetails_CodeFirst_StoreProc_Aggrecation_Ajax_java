🧾 Project Summary: Core Master-Details with Code First, Stored Procedure, Aggregation, and AJAX (JavaScript)
This ASP.NET Core MVC project demonstrates a Master-Details relationship implementation using Entity Framework Core (Code First approach) with an integrated Stored Procedure, AJAX, and data aggregation. The system is designed for managing employee candidates and their associated skills.

🔧 Technologies Used:
ASP.NET Core MVC

Entity Framework Core (Code First)

SQL Server (Stored Procedures)

Bootstrap (UI)

AJAX & jQuery (Dynamic updates)

C#

HTML5 + Razor Pages

📦 Features:
Master-Details Relationship:

Employee (Master) and Skill (Details) via junction table EmployeeSkill.

Code First Modeling:

Models created using C# POCO classes, with relationships mapped using navigation properties.

Stored Procedure Integration:

A stored procedure spInsertSkill is used to insert new skills securely using ExecuteSqlRaw.

AJAX Integration:

AJAX is used to load and submit data without full page reloads (e.g., add skills dynamically).

Data Annotations & Validation:

Client-side and server-side validation using [Required], [StringLength], [DisplayFormat], etc.

Image Upload:

Employees have an image field to upload their photo.

Fresher Field:

Boolean flag to identify freshers for filtering or conditional logic.
