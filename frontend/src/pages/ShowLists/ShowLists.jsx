import "./showlist.css"
import ListSummary from "../../components/ListSummary/ListSummary";
import {useEffect, useState} from "react";
import {Link} from "react-router-dom";

function ShowLists(props) {
    const [taskLists, setTaskLists] = useState([]);

    useEffect(() => {
        async function fetchTaskLists(userId) {
            const url = `https://localhost:44312/api/TaskList/showAllForUser/${userId}`
            const response = await fetch(url);
            const data = await response.json();
            setTaskLists(data);
        }

        if (localStorage.getItem("userId")) {
            fetchTaskLists(localStorage.getItem("userId"));
        }
    }, [props.userId]);

    return (
        <div className="show-lists-container">
            <h2>Your Lists</h2>
            <ul className="lists">
                {taskLists.map((taskList) => (
                    <Link to={`/show-list/${taskList.id}`} className="list-link" key={taskList.id}>
                        <ListSummary name={taskList.name} description={taskList.description}
                                     createdAt={taskList.createdAt}/>
                    </Link>
                ))}
            </ul>
        </div>
    )
}

export default ShowLists;