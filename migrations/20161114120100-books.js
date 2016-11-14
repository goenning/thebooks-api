exports.up = function (db, done) {
    db.createTable('books', {
        id: { type: 'int', primaryKey: true, autoIncrement: true },
        name: { type: 'string', length: 40, notNull: true },
        isbn: { type: 'string', length: 13, notNull: true },
        pages: { type: 'int', notNull: true },
        library_id: { 
            type: 'int', 
            notNull: true,
            foreignKey: {
                name: 'books_libraries_fk',
                table: 'libraries',
                rules: {
                    onDelete: 'CASCADE',
                    onUpdate: 'RESTRICT'
                },
                mapping: {
                    library_id: 'id'
                }
            } 
        },
    }, done);
    done();
};

exports.down = function (db, done) {
  db.dropTable('books', done);
};