

import { Link } from 'react-router-dom'
import { AppContext } from '../Context'
import { useContext } from 'react'

function LeftMenu(){
    const isAuthorized = useContext(AppContext).isAuthorized
    const myPagePath = isAuthorized == true ? `/userpage/${localStorage.getItem('id')}` : '/login'
    return(
        <div className="container">
          <div className="row mb-2">
            <div className="d-flex flex-nowrap" >
                <div style={{textAlign: 'right'}}>
                    <Link to="/news">
                    <img src={process.env.PUBLIC_URL + '/news.png'} className="icon"/>
                    </Link>
                    
                </div>
            
            <div className="d-none d-xl-block d-lg-block">
                <Link to="/news" style={{color: 'black', textDecoration: 'none'}}>
                <p className="fw-normal ms-2 leftMenuPoint" >News</p>
                </Link>
            </div>
          </div>
          </div>

          <div className="row mb-2">
            <div className="d-flex flex-nowrap" >
                <div style={{textAlign: 'right'}}>
                    <Link to={`${myPagePath}`}>
                    <img src={process.env.PUBLIC_URL + '/user.png'} className="icon"/>
                    </Link>
                </div>
            
            <div className="d-none d-xl-block d-lg-block">
               
            <Link to={`${myPagePath}`} style={{color: 'black', textDecoration: 'none'}}>
                <p className="fw-normal ms-2 leftMenuPoint" >My Page</p>
                </Link>
            </div>
          </div>
          </div>
        </div>

    )
}

export default LeftMenu