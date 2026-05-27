import { useState } from "react";
import { loginTenant } from "../services/api";

function Login() {
  const [form, setForm] = useState({ email: "", password: "" });
  const [error, setError] = useState("");

  const handleSubmit = async () => {
    try {
      const res = await loginTenant(form);
      localStorage.setItem("token", res.data.token);
      localStorage.setItem("tenantId", res.data.tenantId);
      localStorage.setItem("companyName", res.data.companyName);
      localStorage.setItem("plan", res.data.plan);
      window.location.href = "/dashboard";
    } catch (err) {
      setError("Invalid email or password!");
    }
  };

  return (
    <div style={{ maxWidth: "400px", margin: "100px auto", padding: "30px", border: "1px solid #ccc", borderRadius: "10px", boxShadow: "0 4px 8px rgba(0,0,0,0.1)" }}>
      <h2 style={{ textAlign: "center", color: "#333" }}>🏢 BusinessHub Login</h2>
      {error && <p style={{ color: "red", textAlign: "center" }}>{error}</p>}
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
      <button
        onClick={handleSubmit}
        style={{ width: "100%", padding: "12px", background: "#4CAF50", color: "white", border: "none", borderRadius: "5px", cursor: "pointer", fontSize: "16px" }}
      >
        Login
      </button>
      <p style={{ textAlign: "center", marginTop: "15px" }}>
        Don't have an account? <a href="/register">Register your company</a>
      </p>
    </div>
  );
}

export default Login;