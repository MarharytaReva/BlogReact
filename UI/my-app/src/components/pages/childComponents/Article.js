

import React from "react"
import UserComp from "./UserComp"

function chunkSubstr(str, size) {
    const numChunks = Math.ceil(str.length / size)
    const chunks = new Array(numChunks)
  
    for (let i = 0, o = 0; i < numChunks; ++i, o += size) {
      chunks[i] = str.substr(o, size)
    }
  
    return chunks
  }

class Article extends React.Component{
    constructor(props){
        super(props)
        this.splitedPlot = []
        this.state = {
            sign: 'Open',
            isOpened: false,
            
            pageNumber: 0,
            display: 'none'
        }
    }
    componentDidMount(){
        this.splitedPlot = chunkSubstr(this.props.article.plot, 700)
    }
    changeDisplay(){
        this.setState(prev => {
            let isOpened = !prev.isOpened
            return{
                ...prev,
                isOpened: isOpened,
                sign: isOpened == true ? 'Close' : 'Open',
                display: isOpened == true ? 'block' : 'none'
            }

        })
    }
    back(){
        this.setState(prev => {
            return{
                ...prev,
                pageNumber: prev.pageNumber == 0 ? 0 : prev.pageNumber - 1
            }
        })
    }
    next(){
        this.setState(prev => {
            let maxIndex = this.splitedPlot.length - 1
            return{
                ...prev,
                pageNumber: prev.pageNumber == maxIndex ? maxIndex : prev.pageNumber + 1
            }
        })
    }
    render(){
        return(
            <>
            <UserComp redir={this.props.redir ? this.props.redir : () => {}} user={this.props.article.user} articleId={this.props.article.articleId} variant={this.props.variant}></UserComp>
            <div className="ms-3 me-3">
            <p className="fw-light fs-3 text-break myLink">{this.props.article.title}</p>
            </div>
            
            <img className="articleImg" src={`data:image/png;base64, ${this.props.article.imageBase}`}/>
            <div className={`fs-4 fw-normal d-${this.state.display} articleText`}>
                {this.splitedPlot[this.state.pageNumber]}
            </div>
            <div className="d-flex flex-nowrap justify-content-center">

            <div className="btn-group" role="group" aria-label="Basic outlined example">
            <button onClick={() => this.back()} type="button" className={`btn btn-outline-primary d-${this.state.display}`}>Back</button>
            <button onClick={() => this.changeDisplay()} type="button" className="btn btn-primary markableBtn">{this.state.sign}</button>
            <button onClick={() => this.next()} type="button" className={`btn btn-outline-primary d-${this.state.display}`}>Next</button>
            </div>

            </div>
            </>
        )
    }
}

export default Article