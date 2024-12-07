import {Link} from "react-router-dom";

function Navbar(props) {
    return (
        <nav className="navbar">
            <div className="navbar-logo">
                <a href="">Task List Manager</a>
            </div>
            <ul className="navbar-links">
                <li>
                    <Link to="/create-list">Create List</Link>
                </li>
                <li>
                    <Link to="/show-lists">Show Lists</Link>
                </li>
                <li>
                    {props.isLoggedIn ?
                        <div class = "logout"
                            style={{cursor: 'pointer'}}
                            onClick={() => {
                                localStorage.removeItem("username");
                                localStorage.removeItem("userId");
                                console.log("Logged out");

                                // Refresh the page
                                window.location.reload();
                            }}
                        >
                            Logout
                        </div>
                        :
                        <Link to="/login">Login</Link>
                    }
                </li>
            </ul>
        </nav>
    );
}

export default Navbar;