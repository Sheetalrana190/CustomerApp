# Customer Management App (WPF + SQLite)

A lightweight desktop application built with C# and WPF for managing customer records, including name, account number, and telephone number. The application uses an SQLite database for data persistence and provides a simple UI for adding, editing, viewing, and deleting customer data.

## 💡 Features

- **Add Customer**: Add new customer entries with validation checks.
- **Edit Customer**: Update existing records directly from the DataGrid.
- **Delete Customer**: Remove a customer with a single click.
- **View All Records**: Display all saved customers in a WPF `DataGrid`.
- **Data Validation**: Ensures proper phone number formats and required field entries.
- **Placeholder UX**: Uses gray placeholder text for user guidance in input fields.
- **SQLite Integration**: Automatic database creation and schema initialization.

## 🛠️ Technologies Used

- **C# (.NET WPF)**
- **SQLite**
- **XAML**
- **Regex (Input Validation)**

## 📂 Database Schema

SQLite database is stored locally (`CustomerDB.sqlite`) and includes one table:

Customers
├── Id (INTEGER, PRIMARY KEY AUTOINCREMENT)
├── Name (TEXT, NOT NULL)
├── AccountNo (TEXT, NOT NULL)
└── Telephone (TEXT, NOT NULL)

markdown
Copy
Edit

## ✅ Validation Rules

- All fields are mandatory.
- Telephone number must match one of these formats:
  - `(123)-456 7890`
  - `+12 345 678 9012`
  - `1234567890`

## 🚀 How to Run

1. Clone or download this project.
2. Open it in **Visual Studio**.
3. Build and run the application (`F5`).
4. On first launch, the SQLite DB file will be created automatically.

## 📸 Screenshots

> *(Include your screenshots here for better visual documentation)*

## 📄 License

This project is for educational and personal use.
