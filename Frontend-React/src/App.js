import './App.css'

import React from 'react'

import { Col, Container, Image, Row } from 'react-bootstrap'

import TestBanner from './components/Banner/TestBanner'
import TodoComponent from './components/Todo/TodoComponent'

const App = () => {
  return (
    <div className="App">
      <Container>
        <Row>
          <Col>
            <Image src="clearPointLogo.png" fluid rounded />
          </Col>
        </Row>
        <Row>
          <Col>
            <TestBanner />
          </Col>
        </Row>
        <Row>
          <Col>
            <TodoComponent />
          </Col>
        </Row>
      </Container>
      <footer className="page-footer font-small teal pt-4">
        <div className="footer-copyright text-center py-3">
          © 2021 Copyright:
          <a href="https://clearpoint.digital" target="_blank" rel="noreferrer">
            clearpoint.digital
          </a>
        </div>
      </footer>
    </div>
  )
}

export default App
