import React, { useState } from 'react'

import TodoItemAction from './TodoItemAction'
import TodoItemTable from './TodoItemTable'

const TodoComponent = () => {
  const [newItem, setNewItem] = useState('')

  const handleAdd = (item) => {
    setNewItem(item)
  }

  return (
    <>
      <TodoItemAction onAdd={handleAdd} />
      <br />
      <br />
      <TodoItemTable onNewItem={newItem} />
    </>
  )
}

export default TodoComponent
