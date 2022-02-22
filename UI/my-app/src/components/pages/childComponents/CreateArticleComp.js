

import React from "react";
import {imageUpload} from './ConvertImageFunc'
import { getArticle, postArticle, putArticle} from "../../../ArticleService";

class CreateArticleComp extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            title: '',
            plot: '',
            articleId: 0,
            userId: parseInt(localStorage.getItem('id')),
            imageBase: '',
            user: null
        }
        this.imageRef = React.createRef();
        this.saveFunc = parseInt(this.props.articleId) != 0 ? putArticle : postArticle
    }
    componentDidMount(){
        if(this.props.articleId != 0){
            getArticle(this.props.articleId, (data) => {
                this.setState({
                    ...data,
                })
            })
        }
    }
    save() {
        imageUpload((img) => {
            let obj = {
                ...this.state,
                imageBase: img
            }
            console.log('obj', obj)
            this.saveFunc(obj, () => {})
        }, this.state.imageBase, this.imageRef.current)
    }
    inputChange(e) {
        const { name, value } = e.target
        this.setState(prev =>{
            return{
                ...prev,
                [name]: value
            }
        }, () => console.log(this.state))
    }
    render() {
        return (
            <div className="col-12 px-lg-5 px-xl-5 px-md-3 mt-5">
                <div className="form-group mb-2" >
                    <label for="exampleFormControlInput1">Title</label>
                    <input name="title" value={this.state.title} onChange={(e) => this.inputChange(e)} type="text" className="form-control" id="exampleFormControlInput1" placeholder="Title" />
                </div>

                <div className="mb-3">
                    <label for="exampleFormControlTextarea1" className="form-label">Plot</label>
                    <textarea name="plot"  value={this.state.plot} onChange={(e) => this.inputChange(e)} className="form-control" id="exampleFormControlTextarea1" rows="8"></textarea>
                </div>

                <div className="mb-3">
                    <label for="formFileSm" className="form-label">Choose image</label>
                    <input ref={this.imageRef} accept="image/png, image/jpeg" className="form-control form-control-sm" id="formFileSm" type="file"/>
                </div>

                <div className="d-flex justify-content-center mb-2 mt-3">

                    <button onClick={() => this.save()} type="button" className="btn markableBtn" style={{ minWidth: '100px' }}>Save</button>

                </div>

            </div>
        )
    }
}
export default CreateArticleComp