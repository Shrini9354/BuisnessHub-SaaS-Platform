import { useState } from "react";
import { registerTenant } from "../services/api";

function Register() {
  const [form, setForm] = useState({
    name: "",
    email: "",
    password: "",
    companyName: "",
    plan: "Free"
  });
  const [message, setMessage] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = async () => {
    try {
      await registerTenant(form);
      setMessage("Company registered successfully! Please login.");
      setTimeout(() => window.location.href = "/login", 2000);
    } catch (err) {
      setError("Registration failed. Try again.");
    }
  };

  return (
    <div style={{ maxWidth: "400px", margin: "50px auto", padding: "30px", border: "1px solid #ccc", borderRadius: "10px", boxShadow: "0 4px 8px rgba(0,0,0,0.1)" }}>
      <h2 style={{ textAlign: "center", color: "#333" }}>🏢 Register Your Company</h2>
      {message && <p style={{ color: "green", textAlign: "center" }}>{message}</p>}
      {error && <p style={{ color: "red", textAlign: "center" }}>{error}</p>}
      
      <div style={{ marginBottom: "15px" }}>
        <label>Your Name</label>
        <input
          type="text"
          value={form.name}
          onChange={(e) => setForm({ ...form, name: e.target.value })}
          style={{ width: "100%", padding: "10px", marginTop: "5px", borderRadius: "5px", border: "1px solid #ccc" }}
        />
      </div>

      <div style={{ marginBottom: "15px" }}>
        <label>Company Name</label>
        <input
          type="text"
          value={form.companyName}
          onChange={(e) => setForm({ ...form, companyName: e.target.value })}
          style={{ width: "100%", padding: "10px", marginTop: "5px", borderRadius: "5px", border: "1px solid #ccc" }}
        />
      </div>

      <div style={{ marginBottom: "15px" }}>
        <label>Email</label>
        <input
          type="email"
          value={form.email}
          onChange={(e) => setForm({ ...form, email: e.target.value })}
          style={{ width: "100%", padding: "10px", marginTop: "5px", borderRadius: "5px", border: "1px solid #ccc" }}
        />
      </div>

      <div style={{ marginBottom: "15px" }}>
        <label>Password</label>
        <input
          type="password"
          value={form.password}
          onChange={(e) => setForm({ ...form, password: e.target.value })}
          style={{ width: "100%", padding: "10px", marginTop: "5px", borderRadius: "5px", border: "1px solid #ccc" }}
        />
      </div>

      <div style={{ marginBottom: "15px" }}>
        <label>Plan</label>
        <select
          value={form.plan}
          onChange={(e) => setForm({ ...form, plan: e.target.value })}
          style={{ width: "100%", padding: "10px", marginTop: "5px", borderRadius: "5px", border: "1px solid #ccc" }}
        >
          <option value="Free">Free</option>
          <option value="Pro">Pro</option>
          <option value="Enterprise">Enterprise</option>
        </select>
      </div>

      <button
        onClick={handleSubmit}
        style={{ width: "100%", padding: "12px", background: "#2196F3", color: "white", border: "none", borderRadius: "5px", cursor: "pointer", fontSize: "16px" }}
      >
        Register Company
      </button>
      <p style={{ textAlign: "center", marginTop: "15px" }}>
        Already have an account? <a href="/login">Login</a>
      </p>
    </div>
  );
}

export default Register;