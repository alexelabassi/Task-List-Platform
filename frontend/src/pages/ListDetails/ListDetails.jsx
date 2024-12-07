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
            const url = "https://localhost:44312/api/TaskList/" + listId
            const response = await fetch(url);
            const data = await response.json();
            setListDetails(data);
        }
        async function fetchTasks() {
            const url = "https://localhost:44312/tasks/" + listId;
            const response = await fetch(url);
            const data = await response.json();
            setTasks(data);
        }
        if(listId) {
            fetchTasks();
            fetchTaskList();
        }
    },[])

    const handleDelete = () => {
        async function deleteTaskList(taskListId, token) {
            const url = `https://localhost:44312/api/tasklist/delete/${taskListId}`;

            const options = {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            };

            try {
                const response = await fetch(url, options);

                if (response.ok) {
                    const data = await response.json();
                    console.log('Task list deleted successfully:', data);
                    window.location.href = "/show-lists"
                } else {
                    if (response.status === 404) {
                        console.error('Task list not found');
                    } else if (response.status === 401) {
                        console.error('Unauthorized. You can only delete your own task list.');
                    } else {
                        console.error('An error occurred:', response.statusText);
                    }
                    return null;
                }
            } catch (error) {
                console.error('Error deleting task list:', error);
                return null;
            }
        }
        deleteTaskList(listId, localStorage.getItem("authToken"));
    }

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
                <button className="delete-button" onClick={handleDelete}>Delete List</button>
            </footer>
        </div>
    )
}

export default ListDetails;