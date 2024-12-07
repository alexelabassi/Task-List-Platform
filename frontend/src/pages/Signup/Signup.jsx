import React, {useState} from 'react';
import {useNavigate} from 'react-router-dom';
import "./signup.css";

const Signup = () => {
    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate(); // Hook for navigation

    const handleSignup = async (e) => {
        e.preventDefault();

        if (!email || !username || !password) {
            setError('All fields are required!');
            return;
        }

        try {
            const response = await fetch('https://localhost:44312/api/account/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({username: username, email: email, password: password})
            });

            if (!response.ok) {
                throw new Error('Failed to register user');
            }

            const data = await response.json();
            localStorage.setItem("username", username);
            localStorage.setItem("userId", data.id);
            console.log('User registered successfully:', data);
        } catch (error) {
            console.error('Error:', error);
        }
        console.log('Signed up successfully!');
        window.location.href = '/show-lists';
    };

    return (
        <div className="signup-container">
            <h2>Sign Up</h2>
            <form onSubmit={handleSignup}>
                <input
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                />
                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                <button type="submit">Sign Up</button>
            </form>
            {error && <p className="error">{error}</p>}
        </div>
    );
};

export default Signup;
