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

> Output : Running & Testing the Application
#### Step 1: Add a New Customer
1.	Enter details into the text fields.
2.	Click "Add Record".
3.	If successful, a message appears, and fields reset.
4.	Click "View Data" to see the new record.
 ![image](https://github.com/user-attachments/assets/a5211d88-9751-490c-a484-91f0f12169cc)

![image](https://github.com/user-attachments/assets/c92620ae-cff5-48a1-b55a-d844ed7fb7b8)

![image](https://github.com/user-attachments/assets/2144d47f-22a6-4a9c-aa1d-032018dd5ce7)

![image](https://github.com/user-attachments/assets/dae74398-9750-4a0b-9e07-66c17fcf100a)
 
#### Step 2: Edit and updating an Existing Customer
1.	Click "View Data".
2.	Click "Edit" on a record. The data will appear in the respective text box for edit.
3.	Modify fields and click "Update".
4.	Click "View Data" to verify the update.

 ![image](https://github.com/user-attachments/assets/8221c7ad-e741-418e-8df7-c6d7e8cfea31)

![image](https://github.com/user-attachments/assets/a5ec7087-b0c4-463a-b5e6-07a3e48d791c)

![image](https://github.com/user-attachments/assets/32e313c1-7d12-46a3-80d0-5e9992ebb880)

     
#### Step 3: Delete a Customer
1.	Click "View Data".
2.	Select a row and click "Delete".
3.	Click "View Data" to confirm removal.
  
![image](https://github.com/user-attachments/assets/b94f70fe-cb5d-4b76-aa10-d413b4b777fc)
 
![image](https://github.com/user-attachments/assets/7b513714-6e98-427d-86ff-917b76cd56f2)

![image](https://github.com/user-attachments/assets/a7149b04-83bd-4e4c-ba16-e5eed3d9640e)
 

 ![image](https://github.com/user-attachments/assets/d83b9342-edfd-4659-a9d8-6ced323fa961)

 ![image](https://github.com/user-attachments/assets/9157d729-5c65-4d65-a3a4-db7750b44990)


#### 4. Validation & Error Handling
•	Phone Number Format Check: Ensures valid formats like +XX XXX XXXX or (XXX)-XXX XXXX.
 ![image](https://github.com/user-attachments/assets/c0026570-18d3-4cf3-bbff-6b186d95946e)

•	Empty Fields Validation: User Clicks "Add Record" Without Entering Data
 ![image](https://github.com/user-attachments/assets/bdb9af0e-f81b-4516-a635-fe0b5b1ebf62)

•	Deleting Validation : User Clicks "Delete" Without Selecting a Record
![image](https://github.com/user-attachments/assets/bda75593-a0d8-4216-b5a5-389e5ddaccf2)


## 📄 License

This project is for educational and personal use.
