import { useState, useEffect } from "react";
import { getEmployees, createEmployee } from "../services/api";
import axios from "axios";

const API_URL = "http://127.0.0.1:5208/api";

function Dashboard() {
  const [employees, setEmployees] = useState([]);
  const [departments, setDepartments] = useState([]);
  const [showEmpForm, setShowEmpForm] = useState(false);
  const [showDeptForm, setShowDeptForm] = useState(false);
  const [activeTab, setActiveTab] = useState("employees");
  const [empForm, setEmpForm] = useState({
    name: "", email: "", position: "", salary: 0, tenantId: 0, departmentId: null
  });
  const [deptForm, setDeptForm] = useState({
    name: "", description: "", tenantId: 0
  });

  const companyName = localStorage.getItem("companyName");
  const plan = localStorage.getItem("plan");
  const token = localStorage.getItem("token");

  const headers = { Authorization: `Bearer ${token}` };

  useEffect(() => {
    fetchEmployees();
    fetchDepartments();
  }, []);

  const fetchEmployees = async () => {
    try {
      const res = await getEmployees();
      setEmployees(res.data);
    } catch (err) {
      console.error(err);
    }
  };

  const fetchDepartments = async () => {
    try {
      const res = await axios.get(`${API_URL}/departments`, { headers });
      setDepartments(res.data);
    } catch (err) {
      console.error(err);
    }
  };

  const handleCreateEmployee = async () => {
    try {
      await createEmployee(empForm);
      setShowEmpForm(false);
      setEmpForm({ name: "", email: "", position: "", salary: 0, tenantId: 0, departmentId: null });
      fetchEmployees();
      alert("Employee added successfully! ✅");
    } catch (err) {
      alert(err.response?.data?.message || "Error adding employee!");
    }
  };

  const handleCreateDept = async () => {
    try {
      await axios.post(`${API_URL}/departments`, deptForm, { headers });
      setShowDeptForm(false);
      setDeptForm({ name: "", description: "", tenantId: 0 });
      fetchDepartments();
      alert("Department created successfully! ✅");
    } catch (err) {
      alert("Error creating department!");
    }
  };

  const handleLogout = () => {
    localStorage.clear();
    window.location.href = "/login";
  };

  return (
    <div style={{ padding: "20px", fontFamily: "Arial" }}>
      {/* Header */}
      <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center", background: "#2196F3", padding: "15px 20px", borderRadius: "10px", color: "white", marginBottom: "20px" }}>
        <div>
          <h1 style={{ margin: 0 }}>🏢 {companyName}</h1>
          <span style={{ background: "white", color: "#2196F3", padding: "3px 10px", borderRadius: "20px", fontSize: "12px", fontWeight: "bold" }}>{plan} Plan</span>
        </div>
        <button onClick={handleLogout} style={{ padding: "8px 20px", background: "red", color: "white", border: "none", borderRadius: "5px", cursor: "pointer" }}>
          Logout
        </button>
      </div>

      {/* Stats */}
      <div style={{ display: "grid", gridTemplateColumns: "repeat(3, 1fr)", gap: "20px", marginBottom: "20px" }}>
        <div style={{ background: "#E3F2FD", padding: "20px", borderRadius: "10px", textAlign: "center" }}>
          <h2 style={{ color: "#2196F3" }}>{employees.length}</h2>
          <p>Total Employees</p>
        </div>
        <div style={{ background: "#E8F5E9", padding: "20px", borderRadius: "10px", textAlign: "center" }}>
          <h2 style={{ color: "#4CAF50" }}>{departments.length}</h2>
          <p>Total Departments</p>
        </div>
        <div style={{ background: "#FFF3E0", padding: "20px", borderRadius: "10px", textAlign: "center" }}>
          <h2 style={{ color: "#FF9800" }}>₹{employees.reduce((sum, e) => sum + e.salary, 0).toLocaleString()}</h2>
          <p>Total Salary</p>
        </div>
      </div>

      {/* Tabs */}
      <div style={{ marginBottom: "20px" }}>
        <button
          onClick={() => setActiveTab("employees")}
          style={{ padding: "10px 20px", background: activeTab === "employees" ? "#2196F3" : "#ddd", color: activeTab === "employees" ? "white" : "black", border: "none", borderRadius: "5px 0 0 5px", cursor: "pointer" }}
        >
          👥 Employees
        </button>
        <button
          onClick={() => setActiveTab("departments")}
          style={{ padding: "10px 20px", background: activeTab === "departments" ? "#2196F3" : "#ddd", color: activeTab === "departments" ? "white" : "black", border: "none", borderRadius: "0 5px 5px 0", cursor: "pointer" }}
        >
          🏛️ Departments
        </button>
      </div>

      {/* Employees Tab */}
      {activeTab === "employees" && (
        <div>
          <div style={{ display: "flex", justifyContent: "space-between", marginBottom: "15px" }}>
            <h2>👥 Employees</h2>
            <button onClick={() => setShowEmpForm(!showEmpForm)} style={{ padding: "10px 20px", background: "#4CAF50", color: "white", border: "none", borderRadius: "5px", cursor: "pointer" }}>
              + Add Employee
            </button>
          </div>

          {showEmpForm && (
            <div style={{ background: "#f5f5f5", padding: "20px", borderRadius: "10px", marginBottom: "20px" }}>
              <h3>Add New Employee</h3>
              <div style={{ display: "grid", gridTemplateColumns: "repeat(2, 1fr)", gap: "10px" }}>
                <input placeholder="Name" value={empForm.name} onChange={(e) => setEmpForm({ ...empForm, name: e.target.value })} style={{ padding: "10px", borderRadius: "5px", border: "1px solid #ccc" }} />
                <input placeholder="Email" value={empForm.email} onChange={(e) => setEmpForm({ ...empForm, email: e.target.value })} style={{ padding: "10px", borderRadius: "5px", border: "1px solid #ccc" }} />
                <input placeholder="Position" value={empForm.position} onChange={(e) => setEmpForm({ ...empForm, position: e.target.value })} style={{ padding: "10px", borderRadius: "5px", border: "1px solid #ccc" }} />
                <input placeholder="Salary" type="number" value={empForm.salary} onChange={(e) => setEmpForm({ ...empForm, salary: parseInt(e.target.value) })} style={{ padding: "10px", borderRadius: "5px", border: "1px solid #ccc" }} />
                <select
                  value={empForm.departmentId || ""}
                  onChange={(e) => setEmpForm({ ...empForm, departmentId: parseInt(e.target.value) })}
                  style={{ padding: "10px", borderRadius: "5px", border: "1px solid #ccc" }}
                >
                  <option value="">Select Department</option>
                  {departments.map((dept) => (
                    <option key={dept.id} value={dept.id}>{dept.name}</option>
                  ))}
                </select>
              </div>
              <button onClick={handleCreateEmployee} style={{ marginTop: "10px", padding: "10px 20px", background: "#2196F3", color: "white", border: "none", borderRadius: "5px", cursor: "pointer" }}>
                Save Employee
              </button>
            </div>
          )}

          <table style={{ width: "100%", borderCollapse: "collapse" }}>
            <thead>
              <tr style={{ background: "#2196F3", color: "white" }}>
                <th style={{ padding: "12px", textAlign: "left" }}>Name</th>
                <th style={{ padding: "12px", textAlign: "left" }}>Email</th>
                <th style={{ padding: "12px", textAlign: "left" }}>Position</th>
                <th style={{ padding: "12px", textAlign: "left" }}>Department</th>
                <th style={{ padding: "12px", textAlign: "left" }}>Salary</th>
              </tr>
            </thead>
            <tbody>
              {employees.map((emp) => (
                <tr key={emp.id} style={{ borderBottom: "1px solid #ddd" }}>
                  <td style={{ padding: "12px" }}>{emp.name}</td>
                  <td style={{ padding: "12px" }}>{emp.email}</td>
                  <td style={{ padding: "12px" }}>{emp.position}</td>
                  <td style={{ padding: "12px" }}>{emp.department?.name || "No Department"}</td>
                  <td style={{ padding: "12px" }}>₹{emp.salary.toLocaleString()}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {/* Departments Tab */}
      {activeTab === "departments" && (
        <div>
          <div style={{ display: "flex", justifyContent: "space-between", marginBottom: "15px" }}>
            <h2>🏛️ Departments</h2>
            <button onClick={() => setShowDeptForm(!showDeptForm)} style={{ padding: "10px 20px", background: "#4CAF50", color: "white", border: "none", borderRadius: "5px", cursor: "pointer" }}>
              + Add Department
            </button>
          </div>

          {showDeptForm && (
            <div style={{ background: "#f5f5f5", padding: "20px", borderRadius: "10px", marginBottom: "20px" }}>
              <h3>Add New Department</h3>
              <div style={{ display: "grid", gridTemplateColumns: "repeat(2, 1fr)", gap: "10px" }}>
                <input placeholder="Department Name" value={deptForm.name} onChange={(e) => setDeptForm({ ...deptForm, name: e.target.value })} style={{ padding: "10px", borderRadius: "5px", border: "1px solid #ccc" }} />
                <input placeholder="Description" value={deptForm.description} onChange={(e) => setDeptForm({ ...deptForm, description: e.target.value })} style={{ padding: "10px", borderRadius: "5px", border: "1px solid #ccc" }} />
              </div>
              <button onClick={handleCreateDept} style={{ marginTop: "10px", padding: "10px 20px", background: "#2196F3", color: "white", border: "none", borderRadius: "5px", cursor: "pointer" }}>
                Save Department
              </button>
            </div>
          )}

          <div style={{ display: "grid", gridTemplateColumns: "repeat(3, 1fr)", gap: "20px" }}>
            {departments.map((dept) => (
              <div key={dept.id} style={{ border: "1px solid #ddd", borderRadius: "10px", padding: "20px", background: "#f9f9f9" }}>
                <h3 style={{ color: "#2196F3" }}>🏛️ {dept.name}</h3>
                <p style={{ color: "#666" }}>{dept.description}</p>
                <p style={{ fontWeight: "bold" }}>👥 {dept.employees?.length || 0} Employees</p>
              </div>
            ))}
          </div>
        </div>
      )}
    </div>
  );
}

export default Dashboard;