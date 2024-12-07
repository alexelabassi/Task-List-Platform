import "./listsummary.css"

function ListSummary(props) {
    const formattedDate = new Date(props.createdAt).toLocaleString("en-US", {
        year: "numeric",
        month: "long",
        day: "numeric",
        hour: "2-digit",
        minute: "2-digit",
    });
    return (
        <li className="list-item">
            <h3>{props.name}</h3>
            <p>{props.description}</p>
            <small>Created on: {formattedDate}</small>
        </li>
    )
}
export default ListSummary;