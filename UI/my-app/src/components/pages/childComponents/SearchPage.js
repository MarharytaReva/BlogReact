

import React from "react";
import { search } from "../../../UserService";
import UserComp from "./UserComp";
class SearchPage extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            searchStr: '',
            list: []
        }
    }
    searchMethod(){
        search(this.state.searchStr, (data) => {
            this.setState(prev =>{
                return{
                    ...prev,
                    list: data.map(item => <UserComp variant="async" user={item}></UserComp>)
                }
            })
        })
    }
    changeEvent(e){
        const value = e.target.value
        this.setState({
            searchStr: value
        })
    }
    render() {
        return (
            <div>
                <div className="d-flex">
                    <input value={this.state.searchStr} onChange={(e) => this.changeEvent(e)} className="form-control me-2" type="text" placeholder="Enter user's name, you're looking for" aria-label="Search"/>
                    <button onClick={() => this.searchMethod()} className="btn btn-outline-primary" >Search</button>
                </div>
                <div>
                    {this.state.list}
                </div>
            </div>
        )
    }
}
export default SearchPage