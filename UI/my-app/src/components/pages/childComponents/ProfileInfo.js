
import { Link } from 'react-router-dom'
function PropfileInfo(props) {
    return (
        <div className=" w-sm-100 w-xs-100 x-md-100" style={{marginRight: '0'}}>
            <img src={`data:image/png;base64, ${props.user.avaBase}`} className="d-inline circleAvaMain" />
            <p className="fs-4 d-inline ms-3">{props.user.name} <Link className="d-inline" to="/edituser"><button type="button" className="btn btn-light d-inline  myLink mt-3" style={{width: '100%'}}>Edit</button></Link></p>
        </div>
    )
}

export default PropfileInfo