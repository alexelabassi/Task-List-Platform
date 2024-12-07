import "./login.css"
import {useState} from "react";
import {useNavigate} from "react-router-dom";

function Login() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        setError("")
        if(!username || !password){
            setError("Please fill out all the fields!")
            return;
        }
        try{
            const url = "https://localhost:44312/api/account/login";
            const response = await fetch(url,{
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({username,password})
            });
            if(!response.ok){
                const errorData = await response.json();
                setError("Wrong username or password");
                throw new Error(errorData.message || errorData);
            }
            const data = await response.json();
            localStorage.setItem("username", data.username);
            localStorage.setItem("userId", data.id);
            console.log(data);
            window.location.href = "/show-lists";
        }
        catch(err){
            setError("Wrong username or password");
            console.error("Login error:",err);
        }

    }

    return (
        <div className="login-container">
            <div className="login-card">
                <h2 className="login-title">Login</h2>
                <form className="login-form" onSubmit ={handleLogin}>
                    <div className="form-group">
                        <label htmlFor="username">Username</label>
                        <input
                            type="text"
                            id="username"
                            name="username"
                            placeholder="Enter your username"
                            required
                            onChange={(e) => setUsername(e.target.value)}
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            placeholder="Enter your password"
                            required
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>
                    {error && <p className="error-message">{error}</p>}
                    <button type="submit" className="login-button">Login</button>
                    <p className="signup-link">
                        Don't have an account? <a href="/signup">Sign up</a>
                    </p>
                </form>
            </div>
        </div>
    );

}
export default Login;