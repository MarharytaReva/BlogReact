
import React from "react";
import {imageUpload} from "./ConvertImageFunc";
import { getUser, putUser } from '../../../UserService'
class EditUser extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            name: '',
            avaBase: ''
        }
        this.id = localStorage.getItem('id')
        this.imageRef = React.createRef();
    }
    save() {
        imageUpload((img) => {
            let obj = {
                ...this.state,
                avaBase: img
            }
            console.log('obj', obj)
            putUser(obj, () => {
                this.props.history.push(`/userpage/${this.id}`)
            })
        }, this.state.avaBase, this.imageRef.current)
    }
    componentDidMount() {

        getUser(this.id, (data) => {
            this.setState({
                ...data,
            })
        })
    }
    inputChange(e) {
        const { name, value } = e.target
        this.setState(prev => {
            return {
                ...prev,
                [name]: value
            }
        }, () => console.log(this.state))
    }
    render() {
        return (
            <div className="col-12 px-lg-5 px-xl-5 px-md-3 mt-5">
                <div className="form-group mb-2" >
                    <label for="exampleFormControlInput1">Name</label>
                    <input name="name" value={this.state.name} onChange={(e) => this.inputChange(e)} type="text" className="form-control" id="exampleFormControlInput1" placeholder="Title" />
                </div>

                <div className="mb-3">
                    <label for="formFileSm" className="form-label">Choose ava image</label>
                    <input ref={this.imageRef} accept="image/png, image/jpeg" className="form-control form-control-sm" id="formFileSm" type="file" />
                </div>

                <div className="d-flex justify-content-center mb-2 mt-3">
                    <div class="btn-group" role="group" aria-label="Basic example">
                        <button onClick={() => this.save()} type="button" className="btn markableBtn" style={{ minWidth: '100px' }}>Save</button>
                        <button onClick={() => this.props.history.push(`/userpage/${this.id}`)} type="button" class="btn btn-light myLink">Back</button>
                    </div>
                </div>
            </div>
        )
    }
}
export default EditUser