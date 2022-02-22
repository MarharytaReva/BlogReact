
import { useContext } from 'react'
import { AppContext } from '../Context'

function LogoutBtn(props) {
  return (
    <button onClick={() => {
     
      localStorage.removeItem('id')
      localStorage.removeItem('token')
      window.location.replace('/')
      props.setContextFunc(false)
    }} className="btn btn-dark">Logout</button>
  )
}

function Nav(props) {
  let btn = useContext(AppContext).isAuthorized == true ? <LogoutBtn setContextFunc={props.func}/> : '' 
    return (
    <nav className="navbar sticky-top navbar-expand-lg navbar-light" style={{ paddingLeft: '8vw', background: 'white' }}>
      <div className="container-fluid">
        <a className="navbar-brand" href="#"><span className="fs-1 brandBlue">My</span> <b className="fs-1">blog</b></a>
        {btn}
      </div>
    </nav>
  )
}

export default Nav