## Task: *Fisher-Yates*

This task focuses on code design and unit-testing for a non-deterministic algorithm.

The *FisherYates* folder contains a bare-bones [ASP.NET Core 7.0](https://learn.microsoft.com/en-us/aspnet/core/getting-started/?view=aspnetcore-7.0&tabs=windows) solution with a "Web app" project and a supporting "Unit test" project. 

The [*Fisher-Yates shuffle*](https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle) is a classic and simple algorithm for shuffling an input sequence of elements. The  [modern version of the algorithm](https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm) (either direction) is the focus of this task.

The provided app has a single endpoint for this algorithm, `GET /fisheryates`, defined as follows:

* **Input:** A string, containing the dasherized list of elements to shuffle (e.g. "D-B-A-C").
* **Response:** A `200 OK` HTTP response with a content-type of `text/plain; charset=utf-8`. The content should be the dasherized output of the algorithm, e.g. "C-D-A-B".

### Your task

In the web application project, add code structures that would support the implementation of the modern version of the algorithm linked above. The code structures should be in **skeleton form only** for holding the algorithm's logic. All method bodies should simply throw a new `NotImplementedException`. 

Then, implement **unit tests only** in the unit tests project to test the algorithm.

**Do:**
* Structure the skeleton for the solution in the web app project in a way that you feel follows best-practices for a modern Asp.Net Core 7.0 web application.
* Write unit tests for correctness, to the extent that you feel is appropriate and follows best-practices. The testing approach should address the issue of non-determinism ("randomness").
* Feel free to use any libraries (e.g. from NuGet) if you feel they'd help you.

**Do not:**
* Do **not** actually implement the algorithm. **You only need to build the skeleton structure and write the unit tests**. All your unit tests should be failing in your (a la [TDD](https://en.wikipedia.org/wiki/Test-driven_development) and its red-green development cycle).
* Do **not** worry about server-side validation (assume the inputs are always valid).
* Do **not** implement any kind of end-to-end/acceptance/Selenium/browser testing (i.e. just write the unit tests).
* Do **not** implement any kind of performance testing or similar (i.e. just focus on unit testing for correctness).

## Task 2 Notes

**Decisions:**
* Based on the provided reference and stating "Web app" it seems like an MVC app, but from functional perspective there is only a single API endpoint. I will build the project structure for a Web Api without Views aka [YAGNI](https://en.wikipedia.org/wiki/You_aren%27t_gonna_need_it).
* While considering YAGNI, leveraging from [SOLID](https://en.wikipedia.org/wiki/SOLID) the Open-closed principle I'll make the solution open for extension by introducing a base IShufflerService
* To ensure integrity between even future shuffler implementations, a common, more generalized test setup is required for IShufflerService

**Additional changes:**
* Upgraded .NET projects to .NET 8 from .NET 7 to extend end of support until November 10, 2026 from May 14, 2024 (expired)
* Upgraded Test project Nuget packages for compatibility reasons
* Added Swashbuckle.AspNetCore Nuget package for Web app project to generate API documentation
* Added StyleCop.Analyzers Nuget package for static code analysis and style, documentation enforcement
* Updated Program.cs to follow best practices
