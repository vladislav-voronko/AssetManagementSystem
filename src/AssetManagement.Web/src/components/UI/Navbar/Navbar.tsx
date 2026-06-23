import { Link } from "react-router-dom"

const Navbar = () => {
    return (
        <div>
            <Link to="/assets">Assets</Link>
            <Link to="/trades">Trades</Link>
            <Link to="/replenishments">Replenishments</Link>
        </div>
    )
}

export default Navbar