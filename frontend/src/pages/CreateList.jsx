import "./createlist.css"
import {useState} from "react";

function CreateList() {
    const [error, setError] = useState(false)
    const [succes, setSucces] = useState(false)
    const [listName, setListName] = useState("")
    const [listDescription, setListDescription] = useState("")

    const handleSubmit = (e) => {
        e.preventDefault();

        if(!localStorage.getItem("userId")){
            alert("You must first be signed in!")
            window.location.href = '/login';
        }

        if (!listName.trim()) {
            setError("The list name can't be empty!");
            setSucces("")
            return;
        }
        async function PostData(){
            const url = "https://localhost:44312/api/tasklist/create"
            const response = await fetch(url, {
                method: "POST",
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("authToken")}`,
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    name: listName,
                    description: listDescription,
                    userId: localStorage.getItem("userId")
                })
            })
        }
        PostData();
        setError(false);
        setSucces(`The list ${listName} has been created!`);
        setListName("");
        setListDescription("")
    }

    return (
        <div className="create-list-container">
            <form className="create-list-form" id="createListForm" onSubmit={handleSubmit}>
                <h2>Create a New List</h2>
                {error && <p id="errorMessage" className="error">{error}</p>}
                {succes && <p id="successMessage" className="success">{succes}</p>}
                <div className="form-group">
                    <label htmlFor="listName">List Name</label>
                    <input
                        type="text"
                        id="listName"
                        value={listName}
                        onChange={(e) => setListName(e.target.value)}
                        placeholder="Enter list name"
                    />
                    <label htmlFor="listDescription">List Description</label>
                    <input type="text" id="listDescription" placeholder={"Enter description"} value ={listDescription}
                           onChange={(e) => setListDescription(e.target.value)}/>
                </div>
                <button type="submit" className="create-list-button">Create List</button>
            </form>
        </div>
    )
}

export default CreateList;