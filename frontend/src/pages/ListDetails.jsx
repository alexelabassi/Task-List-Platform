import {Link, useParams} from "react-router-dom";
import "./listdetails.css"
import {useEffect, useState} from "react";

function formatDate(createdAt) {
    return new Date(createdAt).toLocaleString("en-US", {
        year: "numeric",
        month: "long",
        day: "numeric",
        hour: "2-digit",
        minute: "2-digit",
    });
}

function ListDetails(props) {
    const {listId} = useParams();
    const [tasks, setTasks] = useState([]);
    const [listDetails, setListDetails] = useState([]);
    useEffect(() => {
        async function fetchTaskList(){
            const url = "https://localhost:44387/api/TaskList/" + listId
            const response = await fetch(url);
            const data = await response.json();
            setListDetails(data);
        }
        async function fetchTasks() {
            const url = "https://localhost:44387/tasks/" + listId;
            const response = await fetch(url);
            const data = await response.json();
            setTasks(data);
        }
        if(listId) {
            fetchTasks();
            fetchTaskList();
        }
    },[])
    return (
        <div className="list-details-container">
            <header>
                <h1 className="list-title">{listDetails.name}</h1>
                <p className="list-description">{listDetails.description}</p>
                <p className="list-created-at">{formatDate(listDetails.createdAt)}</p>
            </header>

            <section className="tasks">
                <h2 className="tasks-title">Tasks</h2>
                {tasks.map((task) => (
                    <div className="task-item" key={task.id}>
                        <p>{task.name}</p>
                    </div>
                ))}
            </section>

            <footer>
                <Link to={`/edit-list/${listId}`}><button className="edit-button">Edit list</button></Link>
                <button className="delete-button">Delete List</button>
            </footer>
        </div>
    )
}

export default ListDetails;