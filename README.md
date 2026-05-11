# DiscoveryApp

DiscoveryApp is a cross-platform game discovery application I developed using .NET MAUI. It features a system where users can list and search for games via the RAWG API, as well as manage their own local accounts.

## Features

* **Database & Registration System:** User data (login, registration, passwords) is securely stored locally on the device using SQLite.
* **Password Security:** Strict validation rules (requiring uppercase, lowercase, and numbers) are enforced during registration and password updates.
* **API Integration:** Game data, cover images, and details are fetched asynchronously using the RAWG API.
* **User Interface:** Designed with Dark Mode standards in mind. UI glitches, such as the Windows overflow menu issue, have been resolved. Disruptive pop-up alerts were replaced with elegant, temporary inline status messages.

## Technologies Used

* .NET MAUI (C# & XAML)
* SQLite-net-PCL
* RAWG API
* Asynchronous Programming (Task, async/await)
