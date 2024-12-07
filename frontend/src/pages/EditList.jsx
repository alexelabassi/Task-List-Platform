import "./editlist.css";
import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";

async function getListName(listId) {
    const url = `https://localhost:44312/api/TaskList/${listId}`
    const response = await fetch(url);
    const data = await response.json();
    return data.name;
}


function EditList() {
    const {listId} = useParams()
    const [listName, setListName] = useState("");
    const [initialName, setInitialName] = useState("");
    const [tasks, setTasks] = useState([]);
    const [newTask, setNewTask] = useState("");
    const [taskEdits, setTaskEdits] = useState({});
    useEffect(() => {
        const fetchListName = async () => {
            const name = await getListName(listId);
            setInitialName(name);
            setListName(name);
        };

        fetchListName();
    }, []); // setez numele initial
    useEffect(() => {
        async function fetchTasks() {
            const url = `https://localhost:44312/tasks/${listId}`;
            const response = await fetch(url);
            const data = await response.json();
            setTasks(data);
        }

        fetchTasks();

    }, []);

    const handleTaskEditChange = (taskId, newName) => {
        setTaskEdits(prev => ({
            ...prev,
            [taskId]: newName
        }));
    };

    const handleTaskEdits = async (e) => {
        e.preventDefault();
        for (const [taskId, newName] of Object.entries(taskEdits)) {
            const url = `https://localhost:44312/task/update/${taskId}`;
            const response = await fetch(url, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ name: newName })
            });

            if (!response.ok) {
                alert(`Failed to edit task with ID ${taskId}`);
            } else {
                const updatedTask = await response.json();
                setTasks(tasks.map(task =>
                    task.id === taskId ? updatedTask : task
                ));
            }
        }
        setTaskEdits({});
    };

    const handleSaveTitle = (e) => {
        console.log(listName);
        if (listName == "") {
            setListName(initialName);
            alert("Name can't be empty!")
        } else {
            async function fetchPutListName() {
                const url = `https://localhost:44312/api/TaskList/edit/${listId}`
                const response = await fetch(url, {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({name: listName}),
                })
                if (!response.ok)
                    alert("Failed to save title");
            }

            fetchPutListName();
        }
    }

    const handleAddTask = async () => {
        const url = "https://localhost:44312/task/create";
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "name": newTask,
                "taskListId": listId
            })
        })
        if (!response.ok) {
            alert("Failed to create task");
        } else {
            const newTask = await response.json()
            setTasks([...tasks, newTask])
            setNewTask("")
        }
    }

    const handleDelete = async (taskId) => {
        const url = "https://localhost:44312/task/delete/" + taskId;
        const response = await fetch(url, {
            method: "DELETE"
        })
        if (!response.ok) {
            alert("Failed to delete task");
        } else {
            setTasks(tasks.filter(task=>task.id !== taskId))
        }
    }
    return (
        <div className="edit-list-container">
            <header>
                <input
                    className="list-title-input"
                    placeholder={initialName}
                    onChange={(e) => setListName(e.target.value)}
                />
            </header>
            <button
                className="save-title-button"
                onClick={handleSaveTitle} // Save title changes
            >
                Save Title
            </button>

            <section className="tasks-section">
                <h2 className="tasks-title">Tasks</h2>
                <ul className="tasks-list">
                    {tasks.map((task) => (
                        <li className="task-item" key={task.id}>
                            <input
                                type="text"
                                className="task-name-input"
                                placeholder={task.name}
                                onChange={(e) => handleTaskEditChange(task.id, e.target.value)}
                            />
                            <button className="delete-task-button" onClick={(e) => handleDelete(task.id)}>Delete
                            </button>
                        </li>
                    ))}
                </ul>

                <div className="add-task-section">
                    <input
                        type="text"
                        className="new-task-input"
                        placeholder="New task name"
                        value = {newTask}
                        onChange={(e) => setNewTask(e.target.value)}
                    />
                    <button className="add-task-button" onClick={handleAddTask}>Add Task</button>
                </div>
            </section>
            <footer>
                <button className="save-changes-button" onClick={handleTaskEdits}>
                    Save Task Edits
                </button>
            </footer>
        </div>
    );
}

export default EditList;