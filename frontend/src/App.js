import './App.css';
import Navbar from "./components/Navbar/Navbar.jsx";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import CreateList from "./pages/CreateList.jsx"
import ShowLists from "./pages/ShowLists.jsx"
import ListDetails from "./pages/ListDetails.jsx"
import EditList from "./pages/EditList.jsx"
import Login from "./pages/Login/Login.jsx"
import Signup from "./pages/Signup/Signup.jsx"
import Footer from "./components/Footer/Footer.jsx"
import {useEffect, useState} from "react";

function App() {
    const [isLoggedIn, setIsLoggedIn] = useState(false)
    useEffect(() => {
        if (localStorage.getItem("username")) {
            setIsLoggedIn(true);
        }
    }, []);
    const userId = 1;
    return (
        <div className="App">
            <BrowserRouter>
                <Navbar isLoggedIn={isLoggedIn}/>
                <main>
                    <Routes>
                        <Route path="/create-list" element={<CreateList/>}/>
                        <Route path="/show-lists" element={<ShowLists userId={userId}/>}/>
                        <Route path="/show-list/:listId" element={<ListDetails/>}></Route>
                        <Route path="/edit-list/:listId" element={<EditList/>}></Route>
                        <Route path="/login" element={<Login/>}></Route>
                        <Route path="/signup" element={<Signup/>}></Route>
                    </Routes>
                </main>
                <Footer/>
            </BrowserRouter>
        </div>

    );
}

export default App;
