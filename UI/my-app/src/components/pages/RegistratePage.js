import React from "react";
import { Link } from 'react-router-dom'
import { registrate } from '../../AccountService'
import { defaultImageStr } from './childComponents/ConvertImageFunc'

class RegistratePage extends React.Component{
    constructor(props){
        super(props)
        this.state = {
            name: '',
            login: '',
            password: '',
            confirm: ''
        }
    }
    inputChange(e){
        const { name, value } = e.target
        this.setState(prev =>{
            return{
                ...prev,
                [name]: value
            }
        }, () => console.log(this.state))
    }
    onRegistrate(){
      registrate({
        userId: 0,
        name: this.state.name,
        login: this.state.login,
        passwordHash: this.state.password,
        avaBase: defaultImageStr
      }, () => {
        this.props.history.push('/login')
      })
    }
    render(){
        return(
            <div className="col-12 px-lg-5 px-xl-5 px-md-3">
                          
                          <div className="form-group mb-2" >
                                <label for="exampleFormControlInput1">Name</label>
                                <input name="name" value={this.state.name} onChange={(e) => this.inputChange(e)} type="text" className="form-control" id="exampleFormControlInput1" placeholder="Enter your name"/>
                              </div>
                            <div className="form-group mb-2" >
                                <label for="exampleFormControlInput1">Login</label>
                                <input name="login" value={this.state.login} onChange={(e) => this.inputChange(e)} type="email" className="form-control" id="exampleFormControlInput1" placeholder="name@example.com"/>
                              </div>
                             
                              
                              <div className="form-group mb-2" >
                                <label for="exampleFormControlInput1">Password</label>
                                <input name="password" value={this.state.password} onChange={(e) => this.inputChange(e)} type="password" className="form-control" id="exampleFormControlInput1" placeholder="Enter your password"/>
                              </div>
                              <div className="form-group mb-2" >
                                <label for="exampleFormControlInput1">Confirm Password</label>
                                <input name="confirm" value={this.state.confirm} onChange={(e) => this.inputChange(e)} type="password" className="form-control" id="exampleFormControlInput1" placeholder="Enter your password"/>
                              </div>
                             
                              <div class="d-flex justify-content-center mt-3 mb-3">
                                <div class="btn-group" role="group" aria-label="Basic example">
                                    <button onClick={() => this.onRegistrate()} type="button" class="btn markableBtn">Registrate</button>
                                    <Link to="/login">
                                    <button type="button" class="btn btn-light myLink">Cancel</button>
                                    </Link>
                                  </div>
                              </div>
                             
                        
                      </div>
        )
    }
}
export default RegistratePage