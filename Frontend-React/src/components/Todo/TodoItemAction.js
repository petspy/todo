import React, { useState } from 'react'

import { Button, Col, Container, Form, Row, Stack } from 'react-bootstrap'

import axiosInstance from '../../api/axios'
import TodoItem from '../../api/models/TodoItem'

const TodoItemAction = ({ onAdd }) => {
  const [description, setDescription] = useState('')
  const [error, setError] = useState('')

  const handleDescriptionChange = (event) => {
    setDescription(event.target.value)
  }

  const handleAdd = async () => {
    const newItem = new TodoItem(description, false)

    await axiosInstance
      .post('/todoitems', newItem)
      .then((response) => {
        setDescription('')
        setError('')
        if (onAdd) {
          onAdd(response.data)
        }
      })
      .catch((error) => {
        setError(error.response?.data ?? error.message)
      })
  }

  const handleClear = () => {
    setDescription('')
  }

  return (
    <Container>
      <h1>Add Item</h1>
      <Form.Group as={Row} className="mb-3" controlId="formAddTodoItem">
        <Form.Label column sm="2">
          Description
        </Form.Label>
        <Col md="6">
          <Form.Control
            type="text"
            placeholder="Enter description..."
            value={description}
            onChange={handleDescriptionChange}
          />
        </Col>
        {error && <Form.Text className="text-danger">{error}</Form.Text>}
      </Form.Group>
      <Form.Group as={Row} className="mb-3 offset-md-2" controlId="formAddTodoItem">
        <Stack direction="horizontal" gap={2}>
          <Button variant="primary" onClick={handleAdd}>
            Add Item
          </Button>
          <Button variant="secondary" onClick={handleClear}>
            Clear
          </Button>
        </Stack>
      </Form.Group>
    </Container>
  )
}

export default TodoItemAction
