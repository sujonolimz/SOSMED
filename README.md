# SOSMED

---How to setup---
1. Restore Data SOSMED.bak in folder Database to Microsoft SQL Server 2019 or latest
2. Change connnection string in SOSMED API > appsettings.json
   line of code below:
   - change Data Source to actual server (where Database is restrored)
   - change User Id & Password in destination server

    "ConnectionStrings": {
  "DefaultConnection": "Data Source=DESKTOP-2EDHVVN\\SQLEXPRESS;Initial Catalog=SOSMED;User Id=sa;Password=dinopro;TrustServerCertificate=True"
  },

---How to run---
1. open API project file using visual studio 2022 in folder SOSMED API > SOSMED AP.sln
2. click start project
Note:
if appsettings.json already configure ConnnectionStrings, should be running success
